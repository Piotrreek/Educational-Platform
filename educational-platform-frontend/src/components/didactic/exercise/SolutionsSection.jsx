import { useState } from "react";

import { Rating, Typography } from "@mui/material";
import { StarRate } from "@mui/icons-material";
import RateSolution from "./RateSolution";
import MaterialModal from "../didacticMaterials/MaterialModal";

import classes from "./Exercise.module.css";

const SolutionsSection = ({
  solutions,
  isLoggedIn,
  handleSolutionRateChange,
}) => {
  const [currentSelectedSolution, setCurrentSelectedSolution] = useState();

  return (
    <>
      <section className="content__section">
        <h2>Rozwiązania</h2>
        {!!solutions.length ? (
          <>
            {solutions.map((solution) => (
              <div className={classes.solution} key={solution.id}>
                <p className={classes.solution__date}>
                  Data dodania:
                  <span>
                    {new Date(solution.createdOn).toLocaleDateString("pl-PL")}
                  </span>
                </p>
                <p className={classes.solution__author}>
                  Autor:
                  <span>{solution.author}</span>
                </p>

                <p className={classes.actions}>
                  <a
                    href={`${process.env.REACT_APP_BACKEND_URL}file/exercise/solution/${solution.id}`}
                  >
                    Pobierz
                  </a>
                  <button
                    onClick={() => setCurrentSelectedSolution(solution.id)}
                  >
                    Obejrzyj
                  </button>
                </p>

                <p>
                  <Typography component="legend">Średnia ocena</Typography>
                  <Rating
                    value={solution.averageRating}
                    precision={0.1}
                    emptyIcon={
                      <StarRate style={{ opacity: 0.55, color: "white" }} />
                    }
                    readOnly
                  />
                </p>
                <RateSolution
                  solution={solution}
                  isLoggedIn={isLoggedIn}
                  handleSolutionRateChange={handleSolutionRateChange}
                />
              </div>
            ))}
          </>
        ) : (
          "To ćwiczenie nie posiada jeszcze żadnych rozwiązań. Zaloguj się i wyślij swoje"
        )}
      </section>
      {!!currentSelectedSolution && (
        <MaterialModal
          onClose={() => setCurrentSelectedSolution()}
          files={[{ id: currentSelectedSolution }]}
          initIndex={0}
          contentType="exercise/solution"
        />
      )}
    </>
  );
};

export default SolutionsSection;
