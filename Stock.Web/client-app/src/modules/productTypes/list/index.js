//import { pickBy } from "lodash";
import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";

const initialState = {
  loading: false,
  productTypes: []
};

/* Action types */

const LOADING = "PRODUCT_TYPES_LOADING";
const SET = "PRODUCT_TYPES_SET";
const CREATE = "PRODUCT_TYPES_CREATE";
const UPDATE = "PRODUCT_TYPES_UPDATE";
const REMOVE = "PRODUCT_TYPES_REMOVE";

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

function handleSet(state, { productTypes }) {
  return {
    ...state,
    productTypes
  };
}

function handleNewProductType(state, { productType }) {
  return {
    ...state,
    productTypes: state.productTypes.concat(productType)
  };
}

function handleUpdateProductType(state, { productType }) {
  return {
    ...state,
    productTypes: state.productTypes.map(s => (s.id === productType.id ? productType : s))
  };
}

function handleRemoveProductType(state, { id }) {
  return {
    ...state,
    productTypes: state.productTypes.filter(s => s.id !== id)
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

export function setProductTypes(productTypes) {
  return {
    type: SET,
    productTypes
  };
}

export function getAll() {
  return dispatch => {
    dispatch(setLoading(true));
    return api
      .get("/productType")
      .then(response => {
        dispatch(setProductTypes(response.data.productTypes));
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

/* Selectors */
function base(state) {
  return state.productType.list;
}

export function getLoading(state) {
  return base(state).loading;
}

export function getProductTypes(state) {
  return base(state).productTypes;
}

export function getProductTypeById(state, id) {
  return getProductTypes(state).find(s => s.id === id);
}
