import classes from "./Input.module.css";

const Input = ({
  id,
  name,
  label,
  type,
  value,
  onChange,
  onBlur,
  hasError,
  error,
}) => {
  return (
    <div className={classes["input-container"]}>
      {label && <label htmlFor={id}>{label}</label>}
      <input
        type={type}
        id={id}
        name={name}
        value={value}
        onChange={onChange}
        onBlur={onBlur}
      />
      {label && <span>{hasError && error}</span>}
    </div>
  );
};

export default Input;
