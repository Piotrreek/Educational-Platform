import "./Container.css";

const Container = ({ children, className }) => {
  return (
    <div className={`container ${!!className ? className : ""}`}>
      {children}
    </div>
  );
};

export default Container;
