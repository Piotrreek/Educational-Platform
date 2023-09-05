import { Rating, Typography } from "@mui/material";
import { StarRate } from "@mui/icons-material";
import RateSolution from "./RateSolution";
import { useState } from "react";

const ExerciseSolutionRatingsSection = ({
  averageRating,
  usersRating,
  solutionId,
  isLoggedIn,
}) => {
  const [ratingObject, setRatingObject] = useState({
    averageRating: averageRating,
    usersRating,
  });

  return (
    <>
      <p>
        <Typography component="legend">Średnia ocena</Typography>
        <Rating
          value={ratingObject.averageRating}
          precision={0.1}
          emptyIcon={<StarRate style={{ opacity: 0.55, color: "white" }} />}
          readOnly
        />
      </p>
      <RateSolution
        isLoggedIn={isLoggedIn}
        solutionId={solutionId}
        usersRating={ratingObject.usersRating}
        setRatingObject={setRatingObject}
      />
    </>
  );
};

export default ExerciseSolutionRatingsSection;
