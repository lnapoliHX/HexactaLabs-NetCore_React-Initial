import api from "../../../common/api";
import { goBack } from "connected-react-router";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";
import { toast } from "react-toastify";

/* Actions */
function success(productType) { return { type: ActionTypes.CREATE, productType }; }

export function create(productType) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .post(`/productType/`, productType)
      .then(response => {
        toast.success("El tipo de producto se creó con éxito");
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
