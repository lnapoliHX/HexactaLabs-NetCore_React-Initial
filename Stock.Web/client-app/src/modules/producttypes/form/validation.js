import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  initials: yup.string().required(),
  description: yup.string().required()
});

export default schema;
