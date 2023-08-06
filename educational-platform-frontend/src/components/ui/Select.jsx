import classes from "./Input.module.css";

const Select = ({
  options,
  value,
  onChange,
  onBlur,
  label,
  id,
  name,
  disabled,
  hasError,
  error,
}) => {
  return (
    <div className={classes["input-container"]}>
      <label htmlFor={id}>{label}</label>
      <select
        disabled={disabled}
        className={classes.select}
        id={id}
        name={name}
        value={value}
        onChange={onChange}
        onBlur={onBlur}
      >
        {options.map((option) => (
          <option key={option.value} value={option.value}>
            {option.text}
          </option>
        ))}
      </select>
      {label && <span>{hasError && error}</span>}
    </div>
  );
};

export default Select;
