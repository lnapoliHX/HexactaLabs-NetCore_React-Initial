import { toast } from "react-toastify";
import { goBack } from "connected-react-router";
import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";

/* Actions */
function success(store) {
  return {
    type: ActionTypes.UPDATE,
    store
  };
}

export function update(store) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .put(`/store/${store.id}`, store)
      .then(() => {
        toast.success("La tienda se editó con éxito");
        dispatch(success(store));
        dispatch(setLoading(false));
        return dispatch(goBack());
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
