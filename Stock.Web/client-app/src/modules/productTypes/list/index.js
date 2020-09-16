import { cloneDeep, pickBy } from "lodash";
import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";

const initialState = {
  loading: false,
  productTypes: []
};

/* Action types */

const LOADING = "PRODUCTTYPES_LOADING";
const SET = "PRODUCTTYPES_SET";
const CREATE = "PRODUCTTYPES_CREATE";
const UPDATE = "PRODUCTTYPES_UPDATE";
const REMOVE = "PRODUCTTYPES_REMOVE";

export const ActionTypes = { LOADING, SET, CREATE, UPDATE, REMOVE };

/* Reducer handlers */
function handleLoading(state, { loading }) {
  return {
    ...state,
    loading
  };
}

function handleSet(state, { productTypes }) {
  return {
    ...state,
    productTypes
  };
}

function handleCreate(state, { productType }) {
  return {
    ...state,
    ids: state.ids.concat([productType.id]),
    byId: {
      ...state.byId,
      [productType.id]: cloneDeep(productType)
    }
  };
}

function handleUpdate(state, { productType }) {
  return {
    ...state,
    productTypes: state.productTypes.map(s => (s.id === productType.id ? productType : s))
  };
}

function handleDelete(state, { id }) {
  return {
    ...state,
    productTypes: state.productTypes.filter(s => s.id !== id)
  };
}

const handlers = {
  [LOADING]: handleLoading,
  [SET]: handleSet,
  [CREATE]: handleCreate,
  [UPDATE]: handleUpdate,
  [REMOVE]: handleDelete
};

export default function reducer(state = initialState, action) {
  const handler = handlers[action.type];
  return handler ? handler(state, action) : state;
}

/* Actions */
export function setLoading(status) { return { type: LOADING, loading: status }; }

export function setProductTypes(productTypes) { return { type: SET, productTypes }; }

export function getAll() {
  return dispatch => {
    dispatch(setLoading(true));
    return api
      .get("/productType")
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
  return function(dispatch) {
    return api
      .post("/productType/search", pickBy(filters))
      .then(response => { dispatch(setProductTypes(response.data)); })
      .catch(error => { apiErrorToast(error); });
  };
}

/* Selectors */
function base(state) { return state.productType.list; }
export function getLoading(state) { return base(state).loading; }
export function getProductTypesById(state) { return base(state).byId; }
export function getProductTypeIds(state) { return base(state).ids; }
export function getProductTypeById(state, id) { return getProductTypes(state).find(s => s.id === id); }
export function getProductTypes(state) { return base(state).productTypes; }