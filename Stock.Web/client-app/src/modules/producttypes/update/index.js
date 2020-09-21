import { toast } from "react-toastify";
import { goBack } from "connected-react-router";
import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";

/* Actions */
function success(productype) {
  return {
    type: ActionTypes.UPDATE,
    productype
  };
}

export function update(productype) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .put(`/productype/${productype.id}`, productype)
      .then(() => {
        toast.success("La Categoria se editó con éxito");
        dispatch(success(productype));
        dispatch(setLoading(false));
        return dispatch(goBack());
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
