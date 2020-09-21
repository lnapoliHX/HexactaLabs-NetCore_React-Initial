import api from "../../../common/api";
import { goBack } from "connected-react-router";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";
import { toast } from "react-toastify";

/* Actions */
function success(producttype) {
  return {
    type: ActionTypes.CREATE,
    producttype
  };
}

export function create(producttype) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .post(`/producttype/`, producttype)
      .then(response => {
        toast.success("La categoria se creó con éxito");
        dispatch(success(response.data.data));
        dispatch(setLoading(false));
        return dispatch(goBack());
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
