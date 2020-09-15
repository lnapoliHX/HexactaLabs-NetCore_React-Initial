import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";
import { toast } from "react-toastify";
import { goBack } from "connected-react-router";

/* Actions */
function success(provider) {
  return {
    type: ActionTypes.UPDATE,
    provider
  };
}

export function update(provider) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .put(`/provider/${provider.id}`, provider)
      .then(response => {
        if (response.data.success) {
          toast.success("El proveedor se editó con éxito");
          dispatch(success(response.data.provider));
          dispatch(setLoading(false));
          return dispatch(goBack()); 
        } else {
          toast.error("El Proveedor ya existe.");
          return dispatch(setLoading(false));  
        }
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
