import { useReducer } from "react";

export const FilterAction = {
  universityId: "universityId",
  facultyId: "facultyId",
  subjectId: "subjectId",
  courseId: "courseId",
};

const filterInitialState = {
  universityId: '',
  facultyId: '',
  subjectId: '',
  courseId: '',
};

const reducer = (state, action) => {
  return { ...state, [action.field]: action.payload };
};

const useMaterialFilters = () => {
  const [materialFilters, dispatch] = useReducer(reducer, filterInitialState);

  return { materialFilters, dispatch };
};

export default useMaterialFilters;
