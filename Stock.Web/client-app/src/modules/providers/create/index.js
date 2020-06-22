import api from "../../../common/api";
import { goBack } from "connected-react-router";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";
import { toast } from "react-toastify";

/* Actions */
function success(provider) {
  return {
    type: ActionTypes.CREATE,
    provider
  };
}

export function create(provider) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .post(`/provider/`, provider)
      .then(response => {
        if(response.data.success){
          toast.success("El proveedor se creó con éxito");
          dispatch(success(response.data.data));
          dispatch(setLoading(false));
          return dispatch(goBack());
        }
        toast.error(response.data.message);
        return dispatch(setLoading(false));
      })
      .catch(error => {        
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
