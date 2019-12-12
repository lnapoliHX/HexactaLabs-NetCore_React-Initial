import { toast } from "react-toastify";

export function apiErrorToast(error) {
  if (error.response) {
    toast.error(error.response.data.Message);
  } else if (error.request) {
    toast.error("Server not responding");
  }
}
