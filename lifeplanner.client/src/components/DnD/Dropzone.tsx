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
                    return [...prevThisList, item];
                }
                return prevThisList;
            });

            setOtherList((prevOtherList) => {
                const updatedOtherList = prevOtherList.filter((card) => card.id !== item.id);
                return updatedOtherList;
            });
        },
        collect: (monitor) => ({
            isOver: monitor.isOver(),
        }),
    }));


    return (
        <div
            className={`dropzone ${isOver ? "bg-secondary-content" : ""}  border-neutral border-dashed border-4 w-full h-fit min-h-52 flex items-center space-x-4 p-4 overflow-scroll`}
            ref={drop}
        >
            {thisList.map((card) => (
                <DragnDropCard key={card.id} id={card.id} title={card.title} description={card.description}/>
            ))}
        </div>
    );
};

export default Dropzone;
