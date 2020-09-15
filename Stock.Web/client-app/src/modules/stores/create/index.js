import api from "../../../common/api";
import { goBack } from "connected-react-router";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";
import { toast } from "react-toastify";

/* Actions */
function success(store) {
  return {
    type: ActionTypes.CREATE,
    store
  };
}

export function create(store) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .post(`/store/`, store)
      .then(response => {
        if (response.data.success) {
          toast.success("La tienda se creó con éxito");
          dispatch(success(response.data.store));
          dispatch(setLoading(false));
          return dispatch(goBack());  
        } else {
          //apiErrorToast(error);
          toast.error("La tienda ya existe.");
          return dispatch(setLoading(false));  
        }
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
