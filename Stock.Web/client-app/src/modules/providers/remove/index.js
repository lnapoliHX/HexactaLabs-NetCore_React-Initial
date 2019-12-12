import { replace } from "connected-react-router";
import { setLoading, ActionTypes } from "../list";
import api from "../../../common/api";
import { toast } from "react-toastify";
import { apiErrorToast } from "../../../common/api/apiErrorToast";

/* Actions */
function success(id) {
  return {
    type: ActionTypes.REMOVE,
    id
  };
}

export function remove(id) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .delete(`/provider/${id}`)
      .then(response => {
        if (!response.data.success) {
          toast.error(response.data.message);
          dispatch(setLoading(false));
          return dispatch(replace("/provider"));
        }

        toast.success("Se eliminó el proveedor con éxito");
        dispatch(success(id));
        dispatch(setLoading(false));
        return dispatch(replace("/provider"));
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
