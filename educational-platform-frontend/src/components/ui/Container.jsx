import "./Container.css";

export const Container = ({ children, className }) => {
  return (
    <div className={`container ${!!className ? className : ""}`}>
      {children}
    </div>
  );
};
