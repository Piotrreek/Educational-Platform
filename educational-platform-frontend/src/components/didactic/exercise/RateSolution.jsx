import { useState } from "react";

import { Typography, Rating } from "@mui/material";
import { StarRate } from "@mui/icons-material";
import {
  getSolutionRatingObject,
  rateSolution,
} from "../../../api/exerciseSolutionsApi";

const RateSolution = ({
  isLoggedIn,
  solutionId,
  usersRating,
  setRatingObject,
}) => {
  const [isRating, setIsRating] = useState(false);

  const rateClickHandler = async (_, value) => {
    setIsRating(true);

    if (usersRating !== 0) {
      await rateSolution(solutionId, "DELETE", null);
    } else {
      await rateSolution(solutionId, "POST", { rating: value });
    }

    setRatingObject(await getSolutionRatingObject(solutionId));

    setIsRating(false);
  };

  return (
    <p>
      <Typography component="legend">Twoja ocena</Typography>
      <Rating
        value={usersRating}
        precision={0.5}
        emptyIcon={<StarRate style={{ opacity: 0.55, color: "white" }} />}
        readOnly={isRating || !isLoggedIn}
        onChange={rateClickHandler}
      />
    </p>
  );
};

export default RateSolution;
