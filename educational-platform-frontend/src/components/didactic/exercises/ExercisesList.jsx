import { useEffect, useState } from "react";

import ClipLoader from "react-spinners/ClipLoader";
import ContentOverview from "../common/ContentOverview";

import classes from "../common/Contents.module.css";

const ExercisesList = ({ exerciseName }) => {
  const [error, setError] = useState();
  const [isLoading, setIsLoading] = useState(false);
  const [exercises, setExercises] = useState([]);

  useEffect(() => {
    const fetchExercises = async () => {
      setIsLoading(true);
      try {
        const response = await fetch(
          `${process.env.REACT_APP_BACKEND_URL}exercise?exerciseName=${exerciseName}`
        );
        const data = await response.json();

        setError("");
        setExercises(data);
      } catch (_) {
        setError("Nieudane połączenie z serwerem, spróbuj ponownie za chwilę");
      }

      setIsLoading(false);
    };
    fetchExercises();
  }, [exerciseName]);

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
      {exercises.map((exercise) => (
        <ContentOverview
          key={exercise.id}
          averageRating={exercise.averageRating}
          author={exercise.author}
          name={exercise.name}
          id={exercise.id}
          contents={exercises}
          fileEndpoint="exercise"
          pageEndpoint="exercise"
        />
      ))}
    </div>
  );
};

export default ExercisesList;
