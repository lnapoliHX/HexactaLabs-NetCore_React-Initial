import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  name: yup.string().required(),
  email: yup.string().required(),
  phone: yup.string().required(),
});

export default schema;
