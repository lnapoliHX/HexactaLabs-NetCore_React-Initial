import api from "../../../common/api";
import { replace } from "connected-react-router";
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

function handleError(dispatch, error) {
  apiErrorToast(error);
  
  return dispatch(setLoading(false));
}

export function create(provider) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .post(`/provider/`, provider)
      .then(response => {
        if (!response.data.success) {
          var error = {response: {data: {Message: response.data.message}}};

          return handleError(dispatch, error);
        }

        dispatch(success(response.data.data));
        dispatch(setLoading(false));
        toast.success("El proveedor se creó con éxito");
        
        return dispatch(replace("/provider"));
      })
      .catch(error => {
        return handleError(dispatch, error);
      });
  };
}
