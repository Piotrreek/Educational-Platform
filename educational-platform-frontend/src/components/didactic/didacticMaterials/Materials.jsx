import { useEffect, useState } from "react";

import ClipLoader from "react-spinners/ClipLoader";
import ContentOverview from "../common/ContentOverview";

import classes from "../common/Contents.module.css";

const Materials = ({ filters }) => {
  const [materials, setMaterials] = useState([]);
  const [error, setError] = useState();
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
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
          }didactic-material?${urlSearchParams.toString()}`
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
    <div className={classes.contents}>
      {materials.map((material) => (
        <ContentOverview
          key={material.id}
          averageRating={material.averageRating}
          author={material.author}
          name={material.name}
          id={material.id}
          contents={materials}
          fileEndpoint="material"
          pageEndpoint="didactic-material"
        />
      ))}
    </div>
  );
};

export default Materials;
