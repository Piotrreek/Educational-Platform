import { useEffect } from "react";
import useInput from "../../../hooks/useInput";

import { notEmpty } from "../../../utils/validators";

import Input from "../../ui/Input";

const EntityNameInput = ({ name, label, setValueIsValid }) => {
  const { value, isValid, error, hasError, onBlur, onChange } =
    useInput(notEmpty);

  useEffect(() => {
    setValueIsValid(isValid);
  }, [isValid, setValueIsValid]);

  return (
    <Input
      id={name}
      name={name}
      label={label}
      type="text"
      value={value}
      onChange={onChange}
      onBlur={onBlur}
      hasError={hasError}
      error={error}
    />
  );
};

export default EntityNameInput;
