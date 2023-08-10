import classes from "./Materials.module.css";
import MaterialOverview from "./MaterialOverview";
import { useEffect, useState } from "react";
import ClipLoader from "react-spinners/ClipLoader";

const Materials = () => {
  const [materials, setMaterials] = useState([]);
  const [error, setError] = useState();
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    const fetchMaterials = async () => {
      setIsLoading(true);
      const response = await fetch(
        `${process.env.REACT_APP_BACKEND_URL}didactic-material`
      );
      const data = await response.json();

      setIsLoading(false);
      setMaterials(data);
    };

    try {
      fetchMaterials();
    } catch (_) {}
  }, []);

  if (isLoading)
    return (
      <div className={classes.loader}>
        <ClipLoader color="#fff" loading={isLoading} size={75} />
      </div>
    );

  return (
    <div className={classes.materials}>
      {!isLoading &&
        materials.map((material) => (
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
