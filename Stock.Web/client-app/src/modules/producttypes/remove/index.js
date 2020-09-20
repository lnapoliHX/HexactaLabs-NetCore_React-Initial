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

function handleError(dispatch, error) {
  apiErrorToast(error);

  return dispatch(setLoading(false));
}

export function remove(id) {
  return function (dispatch) {
    dispatch(setLoading(true));
    return api
      .delete(`/producttype/${id}`)
      .then(response => {
        if (!response.data.success) {
          var error = { response: { data: { Message: response.data.message } } };

          return handleError(dispatch, error);
        }

        dispatch(success(id));
        dispatch(setLoading(false));
        toast.success("Se eliminó la categoría con éxito");

        return dispatch(replace("/producttype"));
      })
      .catch(error => {
        return handleError(dispatch, error);
      });
  };
}
