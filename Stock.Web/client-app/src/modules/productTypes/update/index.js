import { toast } from "react-toastify";
import { goBack } from "connected-react-router";
import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";

/* Actions */
function success(productType) { return { type: ActionTypes.UPDATE, productType }; }

export function update(productType) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .put(`/productType/${productType.id}`, productType)
      .then(() => {
        toast.success("El tipo de producto se editó con éxito");
        dispatch(success(productType));
        dispatch(setLoading(false));
        return dispatch(goBack());
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
