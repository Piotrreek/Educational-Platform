import { useState } from "react";

const useInput = (validateValue) => {
  const [value, setValue] = useState("");
  const [touched, setTouched] = useState(false);

  const { error, isValid } = validateValue(value);
  const hasError = !isValid && touched;

  const onChange = (event) => {
    setValue(event.target.value);
  };

  const onBlur = () => {
    setTouched(true);
  };

  const reset = () => {
    setValue("");
    setTouched(false);
  };

  return {
    value,
    isValid,
    hasError,
    error,
    onChange,
    onBlur,
    reset,
  };
};

export default useInput;
