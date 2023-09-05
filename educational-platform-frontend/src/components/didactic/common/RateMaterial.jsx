import { useState } from "react";
import { Rating } from "@mui/material";
import { getToken } from "../../../utils/jwtUtils";
import { StarRate } from "@mui/icons-material";
import useAuth from "../../../hooks/useAuth";

const RateMaterial = ({ rate, handleRateChange, contentId, endPointPart }) => {
  const [isRating, setIsRating] = useState(false);
  const { ctx } = useAuth();
  const isLoggedIn = ctx.claims.isLoggedIn;

  const rateMaterial = async (method, body, id) => {
    try {
      setIsRating(true);
      const response = await fetch(
        `${process.env.REACT_APP_BACKEND_URL}${endPointPart}/${id}/rate`,
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

      handleRateChange(body?.rating ?? 0, data.averageRating, data.lastRatings);
    } catch (_) {}

    setIsRating(false);
  };

  const rateClickHandler = (_, value) => {
    if (rate !== 0) {
      rateMaterial("DELETE", null, contentId);
      return;
    }

    rateMaterial("POST", { rating: value }, contentId);
  };

  return (
    <div className="rate-content">
      <Rating
        name="rate"
        value={rate}
        precision={0.5}
        emptyIcon={<StarRate style={{ opacity: 0.55, color: "white" }} />}
        onChange={rateClickHandler}
        readOnly={isRating || !isLoggedIn}
      />
    </div>
  );
};

export default RateMaterial;
