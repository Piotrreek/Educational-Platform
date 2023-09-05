import { Typography, Rating } from "@mui/material";
import { StarRate } from "@mui/icons-material";

const RatingsSection = ({ ratings, averageRating, noRatingsText }) => {
  return (
    <section className="content__section">
      <h2>Oceny</h2>
      {!!ratings.length ? (
        <>
          <div>
            <Typography component="legend">Średnia ocena</Typography>
            <Rating
              value={averageRating}
              precision={0.1}
              emptyIcon={<StarRate style={{ opacity: 0.55, color: "white" }} />}
              readOnly
              className="rating"
            />
          </div>
          <div>
            Ostatnie oceny
            {ratings.map((rating, id) => (
              <div key={id}>
                <Rating
                  value={rating}
                  precision={0.1}
                  emptyIcon={
                    <StarRate style={{ opacity: 0.55, color: "white" }} />
                  }
                  className="rating"
                  readOnly
                />
              </div>
            ))}
          </div>
        </>
      ) : (
        <>{noRatingsText}</>
      )}
    </section>
  );
};

export default RatingsSection;
