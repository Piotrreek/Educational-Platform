import { useCallback, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import useAuth from "../../../hooks/useAuth";

import DescriptionSection from "../common/DescriptionSection";
import OpinionsSection from "../common/OpinionsSection";
import RateMaterial from "../common/RateMaterial";
import RatingsSection from "../common/RatingsSection";
import SolutionsSection from "./SolutionsSection";
import NewSolutionSection from "./NewSolutionSection";
import MaterialModal from "../didacticMaterials/MaterialModal";

import pdf from "../../../assets/pdf.svg";

import { getToken } from "../../../utils/jwtUtils";

const exerciseInitialState = {
  name: "",
  description: "",
  author: "",
  solutions: [],
  averageRating: 0,
  lastRatings: [],
  usersRate: 0,
};

const Exercise = () => {
  const { ctx } = useAuth();
  const [exercise, setExercise] = useState(exerciseInitialState);
  const [exerciseModalOpened, setExerciseModalOpened] = useState(false);
  const navigate = useNavigate();
  const params = useParams();

  const setSolutions = useCallback((solutions) => {
    setExercise((prev) => {
      const newState = { ...prev, solutions: solutions };

      return newState;
    });
  }, []);

  const handleRateChange = useCallback(
    (newRate, averageRating, lastRatings) => {
      setExercise((prev) => {
        return {
          ...prev,
          averageRating: averageRating,
          usersRate: newRate,
          lastRatings: lastRatings,
        };
      });
    },
    []
  );

  const handleSolutionRateChange = useCallback(
    (solutionId, usersRating, averageRating) => {
      const newState = { ...exercise };

      const indexOfSolutionToChange = newState.solutions.findIndex(
        (e) => e.id === solutionId
      );

      newState.solutions[indexOfSolutionToChange] = {
        ...newState.solutions[indexOfSolutionToChange],
        usersRating: usersRating,
        averageRating: averageRating,
      };

      setExercise(newState);
    },
    [exercise]
  );

  useEffect(() => {
    const getExercise = async () => {
      try {
        const response = await fetch(
          `${process.env.REACT_APP_BACKEND_URL}exercise/${params.id}`,
          {
            credentials: "include",
            headers: {
              Authorization: `Bearer ${getToken()}`,
            },
          }
        );
        if (response.status === 404) {
          navigate("not-found");
        }

        if (!response.ok) {
          // redirect to error page
        }

        const data = await response.json();
        setExercise(data);
      } catch (_) {
        // redirect to error page
      }
    };

    getExercise();
  }, [navigate, params.id]);

  return (
    <div className="content">
      <div className="content__header">
        <h2 className="content__name">{exercise.name}</h2>
        <div className="content__header-content">
          <div className="content__thumbnail">
            <img src={pdf} alt="thumbnail" />
          </div>
          <div className="content__actions">
            <a
              href={`${process.env.REACT_APP_BACKEND_URL}file/exercise/${params.id}`}
            >
              Pobierz
            </a>
            <button onClick={() => setExerciseModalOpened(true)}>
              Obejrzyj
            </button>
            <RateMaterial
              rate={exercise.usersRate}
              handleRateChange={handleRateChange}
              contentId={params.id}
              endPointPart="exercise"
            />
          </div>
        </div>
      </div>
      <div className="content__section content__description-section">
        <DescriptionSection description={exercise.description} />
      </div>
      <SolutionsSection
        solutions={exercise.solutions}
        handleSolutionRateChange={handleSolutionRateChange}
        isLoggedIn={ctx.claims.isLoggedIn}
      />
      {ctx.claims.isLoggedIn && (
        <NewSolutionSection setSolutions={setSolutions} />
      )}
      <RatingsSection
        ratings={exercise.lastRatings}
        averageRating={exercise.averageRating}
        noRatingsText={`To ćwiczenie nie posiada jeszcze żadnych ocen. Możesz je ocenić pod
        przyciskami "Pobierz" i "Obejrzyj" jeśli się zalogujesz.`}
      />
      <OpinionsSection
        opinions={[]}
        setOpinionsList={() => {}}
        noOpinionsText="To ćwiczenie nie posiada jeszcze żadnych opinii. Możesz dodać pod spodem opinię jeśli się zalogujesz"
      />
      {exerciseModalOpened && (
        <MaterialModal
          onClose={() => setExerciseModalOpened(false)}
          files={[{ id: params.id }]}
          initIndex={0}
          contentType="exercise"
        />
      )}
    </div>
  );
};
export default Exercise;
