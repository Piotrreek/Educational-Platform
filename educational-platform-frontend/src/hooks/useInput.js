import { useCallback, useState } from "react";

const useInput = (validateValue, defaultValue = "", isFile = false) => {
  const [value, setValue] = useState(defaultValue);
  const [touched, setTouched] = useState(false);

  const { error, isValid } = validateValue(value);
  const hasError = !isValid && touched;

  const onChange = (event) => {
    if (!isFile) {
      setValue(event.target.value);
      return;
    }

    setValue(event.target.files[0]);
    setTouched(true);
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
