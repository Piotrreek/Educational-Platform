import classes from "./Materials.module.css";
import MaterialOverview from "./MaterialOverview";
import { useEffect, useState } from "react";
import ClipLoader from "react-spinners/ClipLoader";

const Materials = ({ filters }) => {
  const [materials, setMaterials] = useState([]);
  const [error, setError] = useState();
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    const abortController = new AbortController();
    const fetchMaterials = async () => {
      setIsLoading(true);
      try {
        const urlSearchParams = new URLSearchParams();
        !!filters.universityId &&
          urlSearchParams.append("universityId", filters.universityId);
        !!filters.facultyId &&
          urlSearchParams.append("facultyId", filters.facultyId);
        !!filters.subjectId &&
          urlSearchParams.append("universitySubjectId", filters.subjectId);
        !!filters.courseId &&
          urlSearchParams.append("universityCourseId", filters.courseId);

        const response = await fetch(
          `${
            process.env.REACT_APP_BACKEND_URL
          }didactic-material?${urlSearchParams.toString()}`,
          { signal: abortController.signal }
        );
        const data = await response.json();

        setError("");
        setMaterials(data);
      } catch (_) {
        setError("Nieudane połączenie z serwerem, spróbuj ponownie za chwilę");
      }
      setIsLoading(false);
    };

    fetchMaterials();

    return () => abortController.abort();
  }, [filters]);

  if (isLoading) {
    return (
      <div className={classes.loader}>
        <ClipLoader color="#fff" loading={isLoading} size={75} />
      </div>
    );
  }

  if (!!error) {
    return (
      <div className={classes.loader}>
        <p className={classes.error}>{error}</p>
      </div>
    );
  }

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
