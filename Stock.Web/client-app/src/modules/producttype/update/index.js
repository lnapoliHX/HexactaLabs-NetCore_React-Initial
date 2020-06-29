import { toast } from "react-toastify";
import { goBack } from "connected-react-router";
import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";

/* Actions */
function success(producttype) {
  return {
    type: ActionTypes.UPDATE,
    producttype
  };
}

export function update(producttype) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .put(`/producttype/${producttype.id}`, producttype)
      .then(() => {
        toast.success("La Categoria se editó con éxito");
        dispatch(success(producttype));
        dispatch(setLoading(false));
        return dispatch(goBack());
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
