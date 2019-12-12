import { pickBy } from "lodash";
import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";

const initialState = {
  loading: false,
  stores: []
};

/* Action types */

const LOADING = "STORES_LOADING";
const SET = "STORES_SET";
const CREATE = "STORES_CREATE";
const UPDATE = "STORES_UPDATE";
const REMOVE = "STORES_REMOVE";

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

function handleSet(state, { stores }) {
  return {
    ...state,
    stores
  };
}

function handleNewStore(state, { store }) {
  return {
    ...state,
    stores: state.stores.concat(store)
  };
}

function handleUpdateStore(state, { store }) {
  return {
    ...state,
    stores: state.stores.map(s => (s.id === store.id ? store : s))
  };
}

function handleRemoveStore(state, { id }) {
  return {
    ...state,
    stores: state.stores.filter(s => s.id !== id)
  };
}

const handlers = {
  [LOADING]: handleLoading,
  [SET]: handleSet,
  [CREATE]: handleNewStore,
  [UPDATE]: handleUpdateStore,
  [REMOVE]: handleRemoveStore
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

export function setStores(stores) {
  return {
    type: SET,
    stores
  };
}

export function getAll() {
  return dispatch => {
    dispatch(setLoading(true));
    return api
      .get("/store")
      .then(response => {
        dispatch(setStores(response.data));
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
      .post("/store/search", pickBy(filters))
      .then(response => {
        dispatch(setStores(response.data));
      })
      .catch(error => {
        apiErrorToast(error);
      });
  };
}

/* Selectors */
function base(state) {
  return state.store.list;
}

export function getLoading(state) {
  return base(state).loading;
}

export function getStores(state) {
  return base(state).stores;
}

export function getStoreById(state, id) {
  return getStores(state).find(s => s.id === id);
}
