import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  Description: yup.string().required(),
  Initials: yup.string().required()
});

export default schema;
