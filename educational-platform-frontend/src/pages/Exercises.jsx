import { useState } from "react";

import Filters from "../components/didactic/exercises/Filters";
import ContentContainer from "../components/didactic/common/ContentContainer";
import ExercisesList from "../components/didactic/exercises/ExercisesList";

const Exercises = () => {
  const [exerciseName, setExerciseName] = useState("");

  return (
    <ContentContainer>
      <Filters setExerciseName={setExerciseName} />
      <ExercisesList exerciseName={exerciseName} />
    </ContentContainer>
  );
};

export default Exercises;
