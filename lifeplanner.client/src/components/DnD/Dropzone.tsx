import React from "react";
import {useDrop} from "react-dnd";
import DragnDropCard from "./DragnDropCard.tsx";

interface DropzoneProps {
    thisList: any[];
    otherList: any[];
    setThisList: React.Dispatch<React.SetStateAction<any[]>>;
    setOtherList: React.Dispatch<React.SetStateAction<any[]>>;
}

const Dropzone: React.FC<DropzoneProps> = ({thisList, otherList, setThisList, setOtherList}) => {
    const [{isOver}, drop] = useDrop(() => ({
        accept: "CARD",
        drop: (item: any) => {
            setThisList((prevThisList) => {
                // Ensure immutability and check if the item is not already in the list
                if (!prevThisList.find((card) => card.id === item.id)) {
                    console.log("Adding to thisList:", [...prevThisList, item]);
                    return [...prevThisList, item];
                }
                return prevThisList;
            });

            setOtherList((prevOtherList) => {
                const updatedOtherList = prevOtherList.filter((card) => card.id !== item.id);
                console.log("Updated otherList:", updatedOtherList);
                return updatedOtherList;
            });
        },
        collect: (monitor) => ({
            isOver: monitor.isOver(),
        }),
    }));


    return (
        <div
            className={`dropzone ${isOver ? "bg-secondary-content" : ""} border-neutral border-dashed border-2 w-full h-full `}
            ref={drop}
        >
            <div className="flex space-x-8 overflow-scroll">
                {thisList.map((card) => (
                    <DragnDropCard key={card.id} id={card.id} title={card.title} description={card.description}/>
                ))}
            </div>
        </div>
    );
};

export default Dropzone;