import { useState } from "react";

import MaterialModal from "../didacticMaterials/MaterialModal";

import classes from "./Exercise.module.css";
import NewSolutionSection from "./NewSolutionSection";
import ExerciseSolutionRatingsSection from "./ExerciseSolutionRatingsSection";
import ExerciseSolutionReviewsSection from "./ExerciseSolutionReviewsSection";

const SolutionsSection = ({ solutionList, isLoggedIn, exerciseId }) => {
  const [solutions, setSolutions] = useState(solutionList);
  const [currentSelectedSolution, setCurrentSelectedSolution] = useState();

  return (
    <>
      <section className="content__section">
        <h2>Rozwiązania</h2>
        {!!solutions.length ? (
          <>
            {solutions.map((solution, index) => (
              <div className={classes.solution} key={solution.id}>
                <h3 style={{ marginBottom: "15px" }}>
                  Rozwiązanie {index + 1}
                </h3>
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
                <ExerciseSolutionRatingsSection
                  averageRating={solution.averageRating}
                  usersRating={solution.usersRating}
                  solutionId={solution.id}
                  isLoggedIn={isLoggedIn}
                />
                <ExerciseSolutionReviewsSection
                  solutionId={solution.id}
                  initialReviews={solution.reviews}
                  isLoggedIn={isLoggedIn}
                />
              </div>
            ))}
          </>
        ) : (
          "To ćwiczenie nie posiada jeszcze żadnych rozwiązań. Zaloguj się i wyślij swoje"
        )}
      </section>
      {isLoggedIn && (
        <NewSolutionSection
          exerciseId={exerciseId}
          setSolutions={setSolutions}
        />
      )}
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
