import { useEffect } from "react";
import Input from "../../ui/Input";

import classes from "../didacticMaterials/Filters.module.css";
import { useState } from "react";

export const Filters = ({ setExerciseName }) => {
  const [value, setValue] = useState("");

  useEffect(() => {
    const timeout = setTimeout(() => {
      setExerciseName(value);
    }, 1000);

    return () => clearTimeout(timeout);
  }, [value, setExerciseName]);

  return (
    <div>
      <h2>Ćwiczenia</h2>
      <form className={classes.form} style={{ justifyContent: "center" }}>
        <Input
          label="Nazwa"
          name="name"
          id="name"
          value={value}
          onChange={(e) => setValue(e.target.value)}
        />
      </form>
    </div>
  );
};

export default Filters;
