import * as yup from "yup";
import message from "../../config/messages";

const schema = yup.object().shape({
  username: yup.string().required(message.REQUIRED),
  password: yup.string().required(message.REQUIRED)
});

export default schema;
