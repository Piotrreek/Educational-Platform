import classes from "./Materials.module.css";
import MaterialOverview from "./MaterialOverview";
import { useEffect, useState } from "react";

const Materials = () => {
  const [materials, setMaterials] = useState([]);
  const [error, setError] = useState();
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    const fetchMaterials = async () => {
      const response = await fetch(
        `${process.env.REACT_APP_BACKEND_URL}didactic-material`
      );
      const data = await response.json();

      return data;
    };

    try {
      setIsLoading(true);
      fetchMaterials().then((data) => setMaterials(data));
    } catch (_) {}

    setIsLoading(false);
  }, []);

  return (
    <div className={classes.materials}>
      {materials.map((material) => (
        <MaterialOverview
          key={material.id}
          averageRating={material.averageRating}
          author={material.author}
          name={material.name}
          id={material.id}
          materials={materials}
        />
      ))}
    </div>
  );
};

export default Materials;
