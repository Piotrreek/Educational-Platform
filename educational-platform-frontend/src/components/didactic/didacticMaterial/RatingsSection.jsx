import classes from "./Material.module.css";
import { Typography, Rating } from "@mui/material";
import { StarRate } from "@mui/icons-material";

const RatingsSection = ({ ratings, averageRating }) => {
  return (
    <section className={classes.ratings}>
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
              className={classes.rating}
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
                  className={classes.rating}
                  readOnly
                />
              </div>
            ))}
          </div>
        </>
      ) : (
        <>
          Ten materiał nie posiada jeszcze żadnych ocen. Możesz go ocenić pod
          przyciskami "Pobierz" i "Obejrzyj" jeśli się zalogujesz.
        </>
      )}
    </section>
  );
};

export default RatingsSection;
