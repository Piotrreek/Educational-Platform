import { useState } from "react";
import classes from "./Material.module.css";
import { Rating } from "@mui/material";
import { getToken } from "../../../utils/jwtUtils";
import { StarRate } from "@mui/icons-material";

const RateMaterial = ({ rate, handleRateChange, materialId }) => {
  const [isRating, setIsRating] = useState(false);

  const rateMaterial = async (method, body) => {
    try {
      setIsRating(true);
      const response = await fetch(
        `${process.env.REACT_APP_BACKEND_URL}didactic-material/rate`,
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
      rateMaterial("DELETE", { didacticMaterialId: materialId });
      return;
    }

    rateMaterial("POST", { didacticMaterialId: materialId, rating: value });
  };

  return (
    <div className={classes.rate}>
      <Rating
        name="rate"
        value={rate}
        precision={0.5}
        emptyIcon={<StarRate style={{ opacity: 0.55, color: "white" }} />}
        onChange={rateClickHandler}
        readOnly={isRating}
      />
    </div>
  );
};

export default RateMaterial;
