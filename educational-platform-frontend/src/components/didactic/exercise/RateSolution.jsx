import { useState } from "react";

import { Typography, Rating } from "@mui/material";
import { StarRate } from "@mui/icons-material";

import { getToken } from "../../../utils/jwtUtils";

const RateSolution = ({ handleSolutionRateChange, solution, isLoggedIn }) => {
  const [isRating, setIsRating] = useState(false);

  const rateMaterial = async (method, body) => {
    try {
      setIsRating(true);
      const response = await fetch(
        `${process.env.REACT_APP_BACKEND_URL}exercise/solution/${solution.id}/rate`,
        {
          method: method,
          credentials: "include",
          headers: {
            Authorization: `Bearer ${getToken()}`,
            "Content-Type": "application/json",
          },
          body: JSON.stringify(body),
        }
      );

      if (!response.ok) {
        setIsRating(false);
        return;
      }

      const data = await response.json();

      handleSolutionRateChange(
        solution.id,
        body?.rating ?? 0,
        data.averageRating
      );
    } catch (_) {}

    setIsRating(false);
  };

  const rateClickHandler = (_, value) => {
    if (solution.usersRating !== 0) {
      rateMaterial("DELETE", null, solution.id);
      return;
    }

    rateMaterial("POST", { rating: value }, solution.id);
  };

  return (
    <p>
      <Typography component="legend">Twoja ocena</Typography>
      <Rating
        value={solution.usersRating}
        precision={0.5}
        emptyIcon={<StarRate style={{ opacity: 0.55, color: "white" }} />}
        readOnly={isRating || !isLoggedIn}
        onChange={rateClickHandler}
      />
    </p>
  );
};

export default RateSolution;
