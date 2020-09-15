import { toast } from "react-toastify";
import { goBack } from "connected-react-router";
import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";

/* Actions */
function success(productType) {
  return {
    type: ActionTypes.UPDATE,
    productType
  };
}

export function update(productType) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .put(`/productType/${productType.id}`, productType)
      .then(response => {
        if (response.data.success) {
          toast.success("El Tipo de Producto se editó con éxito");
          dispatch(success(response.data.productType));
          dispatch(setLoading(false));
          return dispatch(goBack()); 
        } else {
          toast.error("El Tipo de Producto ya existe.");
          return dispatch(setLoading(false));  
        }
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
