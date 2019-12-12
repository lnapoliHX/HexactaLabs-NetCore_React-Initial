import * as yup from "yup";

yup.addMethod(yup.number, "format", function(formats, parseStrict) {
  return this.transform(function(value, original) {
    return original === "" ? null : value;
  });
});
