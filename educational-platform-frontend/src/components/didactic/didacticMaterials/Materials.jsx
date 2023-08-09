import classes from "./Materials.module.css";
import MaterialOverview from "./MaterialOverview";

const Materials = () => {
  return (
    <div className={classes.materials}>
      <MaterialOverview />
      <MaterialOverview />
      <MaterialOverview />
      <MaterialOverview />
    </div>
  );
};

export default Materials;
