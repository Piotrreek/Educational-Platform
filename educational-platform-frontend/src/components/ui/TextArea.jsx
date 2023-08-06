import classes from "./Input.module.css";

const TextArea = ({
  id,
  label,
  value,
  onChange,
  name,
  onBlur,
  hasError,
  error,
}) => {
  return (
    <div className={classes["input-container"]}>
      <label htmlFor={id}>{label}</label>
      <textarea
        className={classes["text-area"]}
        id={id}
        name={name}
        value={value}
        onChange={onChange}
        onBlur={onBlur}
      ></textarea>
      {label && (
        <span className={classes["text-area__span"]}>{hasError && error}</span>
      )}
    </div>
  );
};

export default TextArea;
