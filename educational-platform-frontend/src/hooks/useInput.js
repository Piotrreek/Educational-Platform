import { useCallback, useState } from "react";

const useInput = (validateValue, defaultValue = "") => {
  const [value, setValue] = useState(defaultValue);
  const [touched, setTouched] = useState(false);

  const { error, isValid } = validateValue(value);
  const hasError = !isValid && touched;

  const onChange = (event) => {
    setValue(event.target.value);
  };

  const onBlur = () => {
    setTouched(true);
  };

  const reset = useCallback(() => {
    setValue("");
    setTouched(false);
  }, []);

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
