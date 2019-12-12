import { cloneDeep, pickBy } from "lodash";
import { normalize } from "../../../common/helpers/normalizer";
import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";

const initialState = {
  loading: false,
  ids: [],
  byId: {}
};

/* Action types */

const LOADING = "PROVIDERS_LOADING";
const SET = "PROVIDERS_SET";
const CREATE = "PROVIDERS_CREATE";
const UPDATE = "PROVIDERS_UPDATE";
const REMOVE = "PROVIDERS_REMOVE";

export const ActionTypes = {
  LOADING,
  SET,
  CREATE,
  UPDATE,
  REMOVE
};

/* Reducer handlers */
function handleLoading(state, { loading }) {
  return {
    ...state,
    loading
  };
}

function handleSet(state, { providers }) {
  return {
    ...state,
    ids: providers.map(provider => provider.id),
    byId: normalize(providers)
  };
}

function handleNewProvider(state, { provider }) {
  return {
    ...state,
    ids: state.ids.concat([provider.id]),
    byId: {
      ...state.byId,
      [provider.id]: cloneDeep(provider)
    }
  };
}

function handleUpdateProvider(state, { provider }) {
  return {
    ...state,
    byId: { ...state.byId, [provider.id]: cloneDeep(provider) }
  };
}

function handleRemoveProvider(state, { id }) {
  return {
    ...state,
    ids: state.ids.filter(providerId => providerId !== id),
    byId: Object.keys(state.byId).reduce(
      (acc, providerId) =>
        providerId !== `${id}`
          ? { ...acc, [providerId]: state.byId[providerId] }
          : acc,
      {}
    )
  };
}

const handlers = {
  [LOADING]: handleLoading,
  [SET]: handleSet,
  [CREATE]: handleNewProvider,
  [UPDATE]: handleUpdateProvider,
  [REMOVE]: handleRemoveProvider
};

export default function reducer(state = initialState, action) {
  const handler = handlers[action.type];
  return handler ? handler(state, action) : state;
}

/* Actions */
export function setLoading(status) {
  return {
    type: LOADING,
    loading: status
  };
}

export function setProviders(providers) {
  return {
    type: SET,
    providers
  };
}

export function getAll() {
  return dispatch => {
    dispatch(setLoading(true));
    return api
      .get("/provider")
      .then(response => {
        dispatch(setProviders(response.data));
        return dispatch(setLoading(false));
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}

export function getById(id) {
  return getAll({ id });
}

export function fetchByFilters(filters) {
  return function(dispatch) {
    return api
      .post("/provider/search", pickBy(filters))
      .then(response => {
        dispatch(setProviders(response.data));
      })
      .catch(error => {
        apiErrorToast(error);
      });
  };
}

/* Selectors */
function base(state) {
  return state.provider.list;
}

export function getLoading(state) {
  return base(state).loading;
}

export function getProvidersById(state) {
  return base(state).byId;
}

export function getProviderIds(state) {
  return base(state).ids;
}

export function getProviderById(state, id) {
  return getProvidersById(state)[id] || {};
}

function makeGetProvidersMemoized() {
  let cache;
  let value = [];
  return state => {
    if (cache === getProvidersById(state)) {
      return value;
    }
    cache = getProvidersById(state);
    value = Object.values(getProvidersById(state));
    return value;
  };
}

export const getProviders = makeGetProvidersMemoized();
