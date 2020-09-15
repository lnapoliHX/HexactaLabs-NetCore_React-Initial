import api from "../../../common/api";
import { goBack } from "connected-react-router";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";
import { toast } from "react-toastify";

/* Actions */
function success(productType) {
  return {
    type: ActionTypes.CREATE,
    productType
  };
}

export function create(productType) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .post(`/productType/`, productType)
      .then(response => {
        if (response.data.success) {
          toast.success("El Tipo de Producto se creó con éxito");
          dispatch(success(response.data.productType));
          dispatch(setLoading(false));
          return dispatch(goBack());  
        } else {
          //apiErrorToast(error);
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
