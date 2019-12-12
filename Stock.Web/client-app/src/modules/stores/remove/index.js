import { replace } from "connected-react-router";
import { toast } from "react-toastify";
import { setLoading, ActionTypes } from "../list";
import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";

const OK_STATUS = 200;

/* Actions */
function success(id) {
  return {
    type: ActionTypes.REMOVE,
    id
  };
}

export function remove(id) {
  return function (dispatch) {
    dispatch(setLoading(true));
    return api
      .delete(`/store/${id}`)
      .then(response => {
        if (response.status !== OK_STATUS) {
          toast.error(response.data.message);
          dispatch(setLoading(false));
          return dispatch(replace("/store"));
        }

        toast.success("Se eliminó la tienda con éxito");
        dispatch(success(id));
        dispatch(replace("/store"));
        return dispatch(setLoading(false));
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
