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
  hide,
}) => {
  console.log(hasError);
  return (
    <div className={classes["input-container"]}>
      {label && (
        <label
          htmlFor={id}
          className={`${hide ? classes.pointer : ""}`}
          dangerouslySetInnerHTML={{ __html: label }}
        />
      )}
      <input
        type={type}
        id={id}
        name={name}
        value={value}
        onChange={onChange}
        onBlur={onBlur}
        className={`${hide ? classes.hidden : ""}`}
      />
      {label && <span>{hasError && error}</span>}
    </div>
  );
};

export default Input;
