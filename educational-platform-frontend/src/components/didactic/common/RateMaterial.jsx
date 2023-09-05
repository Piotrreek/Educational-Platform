import { useState } from "react";
import { Rating } from "@mui/material";
import { StarRate } from "@mui/icons-material";
import useAuth from "../../../hooks/useAuth";
import { rateContent } from "../../../api/ratingsApi";

const RateMaterial = ({ rate, handleRateChange, contentId, endPointPart }) => {
  const [isRating, setIsRating] = useState(false);
  const [currentRate, setCurrentRate] = useState(rate);
  const { ctx } = useAuth();
  const isLoggedIn = ctx.claims.isLoggedIn;

  const rateMaterial = async (method, body, id) => {
    setIsRating(true);
    const data = await rateContent(endPointPart, id, method, body);

    setCurrentRate(body?.rating ?? 0);
    handleRateChange(data.averageRating, data.lastRatings);

    setIsRating(false);
  };

  const rateClickHandler = (_, value) => {
    if (currentRate !== 0) {
      rateMaterial("DELETE", null, contentId);
      return;
    }

    rateMaterial("POST", { rating: value }, contentId);
  };

  return (
    <div className="rate-content">
      <Rating
        name="rate"
        value={currentRate}
        precision={0.5}
        emptyIcon={<StarRate style={{ opacity: 0.55, color: "white" }} />}
        onChange={rateClickHandler}
        readOnly={isRating || !isLoggedIn}
      />
    </div>
  );
};

export default RateMaterial;
