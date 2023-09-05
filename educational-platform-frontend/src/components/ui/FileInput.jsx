import classes from "./Input.module.css";

const FileInput = ({ id, name, label, onChange, onBlur, hasError, error }) => {
  return (
    <div className={classes["input-container"]}>
      {label && (
        <label
          htmlFor={id}
          className={classes.pointer}
          dangerouslySetInnerHTML={{ __html: label }}
        />
      )}
      <input
        type="file"
        id={id}
        name={name}
        onChange={onChange}
        onBlur={onBlur}
        className={classes.hidden}
      />
      {label && <span>{hasError && error}</span>}
    </div>
  );
};

export default FileInput;
