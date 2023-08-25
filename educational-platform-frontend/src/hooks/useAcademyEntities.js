import { useReducer } from "react";
import { useSearchParams } from "react-router-dom";

export const FilterAction = {
  universityId: "universityId",
  facultyId: "facultyId",
  subjectId: "subjectId",
  courseId: "courseId",
};

const reducer = (state, action) => {
  return { ...state, [action.field]: action.payload };
};

const useAcademyEntities = () => {
  const [searchParams] = useSearchParams();

  const entitiesInitialState = {
    universityId: searchParams.get("universityId") ?? "",
    facultyId: searchParams.get("facultyId") ?? "",
    subjectId: searchParams.get("subjectId") ?? "",
    courseId: searchParams.get("courseId") ?? "",
  };

  const [academyEntities, dispatch] = useReducer(reducer, entitiesInitialState);

  return { academyEntities, dispatch };
};

export default useAcademyEntities;
