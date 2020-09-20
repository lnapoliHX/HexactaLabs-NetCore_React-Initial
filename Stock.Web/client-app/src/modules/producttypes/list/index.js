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

const LOADING = "PRODUCTTYPES_LOADING";
const SET = "PRODUCTTYPES_SET";
const CREATE = "PRODUCTTYPES_CREATE";
const UPDATE = "PRODUCTTYPES_UPDATE";
const REMOVE = "PRODUCTTYPES_REMOVE";

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

function handleSet(state, { producttypes }) {
  return {
    ...state,
    ids: producttypes.map(productType => productType.id),
    byId: normalize(producttypes)
  };
}

function handleNewProductType(state, { producttype }) {
  return {
    ...state,
    ids: state.ids.concat([producttype.id]),
    byId: {
      ...state.byId,
      [producttype.id]: cloneDeep(producttype)
    }
  };
}

function handleUpdateProductType(state, { producttype }) {
  return {
    ...state,
    byId: { ...state.byId, [producttype.id]: cloneDeep(producttype) }
  };
}

function handleRemoveProductType(state, { id }) {
  return {
    ...state,
    ids: state.ids.filter(productTypeId => productTypeId !== id),
    byId: Object.keys(state.byId).reduce(
      (acc, productTypeId) =>
        productTypeId !== `${id}`
          ? { ...acc, [productTypeId]: state.byId[productTypeId] }
          : acc,
      {}
    )
  };
}

const handlers = {
  [LOADING]: handleLoading,
  [SET]: handleSet,
  [CREATE]: handleNewProductType,
  [UPDATE]: handleUpdateProductType,
  [REMOVE]: handleRemoveProductType
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

export function setProductTypes(producttypes) {
  return {
    type: SET,
    producttypes
  };
}

export function getAll() {
  return dispatch => {
    dispatch(setLoading(true));
    return api
      .get("/producttype")
      .then(response => {
        dispatch(setProductTypes(response.data));
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
  return function (dispatch) {
    return api
      .post("/producttype/search", pickBy(filters))
      .then(response => {
        dispatch(setProductTypes(response.data));
      })
      .catch(error => {
        apiErrorToast(error);
      });
  };
}

/* Selectors */
function base(state) {
  return state.producttype.list;
}

export function getLoading(state) {
  return base(state).loading;
}

export function getProductTypesById(state) {
  return base(state).byId;
}

export function getProductTypeIds(state) {
  return base(state).ids;
}

export function getProductTypeById(state, id) {
  return getProductTypesById(state)[id] || {};
}

function makeGetProductTypesMemoized() {
  let cache;
  let value = [];
  return state => {
    if (cache === getProductTypesById(state)) {
      return value;
    }
    cache = getProductTypesById(state);
    value = Object.values(getProductTypesById(state));
    return value;
  };
}

export const getProductTypes = makeGetProductTypesMemoized();
